import { yupResolver } from "@hookform/resolvers/yup";
import { Box, Button, Container, Typography } from "@mui/material";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "src/app/hooks";
import { InputField } from "src/components/FormFields/InputField";
import { IBaseAddOrUpdateBodyRequest } from "src/models/Bases";
import { IStatus } from "src/models/Common/IStatus";
import { IPaymentAccountCreateOrUpdateModel } from "src/models/PaymentAccount";
import * as yup from "yup";
import {
  paymentAccountActions,
  selectPaymentAccountCreateOrUpdateData,
  selectPaymentAccountCreateOrUpdateStatus,
} from "./paymentAccountSlice";
export interface IPaymentAccountAddOrUpdateProps {}
export type IPaymentAccountAddOrUpdateParams = {
  id?: string;
};
const schema = yup.object({
  data: yup.object().shape({
    // name: yup.string().required("Please enter name."),
    // icon: yup.boolean().required("Please enter icon."),
    // initialMoney: yup.string().required("Please enter initial money."),
  }),
});

export default function PaymentAccountAddOrUpdate(
  props: IPaymentAccountAddOrUpdateProps
) {
  const { id } = useParams<IPaymentAccountAddOrUpdateParams>();
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const status = useAppSelector(selectPaymentAccountCreateOrUpdateStatus);
  const data = useAppSelector(selectPaymentAccountCreateOrUpdateData);
  const {
    control,
    handleSubmit,
    register,
    reset,
    formState: { isSubmitting, errors },
  } = useForm<
    IBaseAddOrUpdateBodyRequest<
      IPaymentAccountCreateOrUpdateModel & { country: any }
    >,
    object
  >({
    defaultValues: {
      data: {
        name: "",
        initialMoney: 0,
        isReport: true,
      },
    },
    resolver: yupResolver(schema),
  });
  // get payment account by id
  useEffect(() => {
    if (id != null) {
      dispatch(paymentAccountActions.fetchPaymentAccount(id));
    } else {
      reset({
        data: {
          name: "",
          initialMoney: 0,
          isReport: true,
        },
      });
    }
  }, [dispatch]);

  useEffect(
    () => () => {
      dispatch(paymentAccountActions.resetPaymentAccountData());
    },
    []
  );

  useEffect(() => {
    console.log("success", status);
    switch (status) {
      case IStatus.Success:
        dispatch(paymentAccountActions.resetPaymentAccountStatus());
        if (id != null) navigate(`/admin/payment-account`);
        break;
      case IStatus.Error:
        // navigate(`/admin/payment-account/add`);
        break;

      default:
        break;
    }
  }, [status, dispatch]);

  // reset form data khi sau khi fetchPaymentAccount
  useEffect(() => {
    if (data == null) {
      reset({
        data: {
          name: "",
          initialMoney: 0,
          isReport: true,
        },
      });
      return;
    }
    reset({ data: data });
    console.info("data", data);
  }, [data]);
  const onSubmit = (
    object: IBaseAddOrUpdateBodyRequest<IPaymentAccountCreateOrUpdateModel>
  ) => {
    dispatch(paymentAccountActions.savePaymentAccount(object.data));
  };
  return (
    <>
      <Container maxWidth="lg">
        <Box
          display="flex"
          mb={3}
          mt={2}
          alignItems="center"
          justifyContent="center"
        >
          <Box>
            <Typography
              variant="h3"
              component="h3"
              gutterBottom
              align="justify"
            >
              {id == null
                ? "Add PaymentAccount"
                : `Update PaymentAccount ${id}`}
            </Typography>
          </Box>
        </Box>
        <form onSubmit={handleSubmit(onSubmit)}>
          <InputField
            id={`data.initialMoney`}
            name={`data.initialMoney`}
            control={control}
            label={`Initial Money`}
            defaultValue={1}
            // type="number"
          />
          <InputField
            id={`data.name`}
            name={`data.name`}
            control={control}
            label={`PaymentAccount name`}
          />
          {/* <CheckboxField
            // id={`data.isReport`}
            name={`data.isReport`}
            control={control}
            label={`isReport`}
            defaultChecked={false}
          /> */}
          <Box
            sx={{
              display: "flex",
              justifyContent: "center",
            }}
          >
            <Button type="submit">Submit</Button>
          </Box>
        </form>
      </Container>
    </>
  );
}
