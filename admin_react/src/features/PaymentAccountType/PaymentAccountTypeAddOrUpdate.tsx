import { yupResolver } from "@hookform/resolvers/yup";
import { Box, Button, Container, Typography } from "@mui/material";
import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import { InputField } from "src/components/FormFields/InputField";
import { IBaseAddOrUpdateBodyRequest } from "src/models/Bases";
import { IStatus } from "src/models/Common/IStatus";
import { IPaymentAccountTypeCreateOrUpdateModel } from "src/models/PaymentAccountType/Requests/IPaymentAccountTypeCreateOrUpdateModel";
import * as yup from "yup";
import { useAppDispatch, useAppSelector } from "../../app/hooks";
import {
  paymentAccountTypeActions,
  selectPaymentAccountTypeCreateOrUpdateData,
  selectPaymentAccountTypeCreateOrUpdateStatus,
} from "./paymentAccountTypeSlice";
export interface IPaymentAccountTypeAddOrUpdateProps {}
export type IPaymentAccountTypeAddOrUpdateParams = {
  id?: string;
};
const schema = yup.object().shape({
  data: yup.object().shape({
    code: yup.string().required("Please enter code."),
    name: yup.string().required("Please enter name."),
    // icon: yup.string().required("Please enter icon."),
  }),
});

export default function PaymentAccountTypeAddOrUpdate(
  props: IPaymentAccountTypeAddOrUpdateProps
) {
  const { id } = useParams<IPaymentAccountTypeAddOrUpdateParams>();
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const status = useAppSelector(selectPaymentAccountTypeCreateOrUpdateStatus);
  const data = useAppSelector(selectPaymentAccountTypeCreateOrUpdateData);
  // xử lý status add or update page
  useEffect(() => {
    console.log("status", status);
    switch (status) {
      case IStatus.Success:
        console.log("success");
        dispatch(paymentAccountTypeActions.resetPaymentAccountTypeStatus());
        if (id != null) navigate(`/admin/payment-account-type`);
        break;
      case IStatus.Error:
        navigate(`/admin/payment-account-type/add`);
        break;

      default:
        break;
    }
  }, [status, dispatch]);

  // get payment account type by id
  useEffect(() => {
    if (id != null) {
      dispatch(paymentAccountTypeActions.fetchPaymentAccountType(id));
    }
  }, [dispatch]);

  // reset form data khi sau khi fetchPaymentAccountType
  useEffect(() => {
    reset({ data: data });
  }, [data]);

  const {
    control,
    handleSubmit,
    register,
    reset,
    formState: { isSubmitting, errors },
  } = useForm<
    IBaseAddOrUpdateBodyRequest<IPaymentAccountTypeCreateOrUpdateModel>,
    object
  >({
    defaultValues: {
      data: {
        code: "",
        name: "",
        id: "",
      },
    },
    resolver: yupResolver(schema),
  });
  const onSubmit = (
    object: IBaseAddOrUpdateBodyRequest<IPaymentAccountTypeCreateOrUpdateModel>
  ) => {
    dispatch(paymentAccountTypeActions.savePaymentAccountType(object.data));
  };
  return (
    <>
      <Container maxWidth="lg">
        <Box
          display="flex"
          alignItems="center"
          justifyContent="center"
          mb={3}
          style={{ textAlign: "center" }}
        >
          <Box justifyContent="center">
            <Typography variant="h3" component="h3" gutterBottom>
              {id == null
                ? `Create PaymentAccountType`
                : `Update PaymentAccountType ${data?.name}`}
            </Typography>
          </Box>
        </Box>
        <form onSubmit={handleSubmit(onSubmit)}>
          <InputField
            id={`data.code`}
            name={`data.code`}
            control={control}
            label={`PaymentAccountType code`}
          />
          <InputField
            id={`data.name`}
            name={`data.name`}
            control={control}
            label={`PaymentAccountType name`}
          />
          {/* <SelectField
            name={`data.icon`}
            control={control}
            label={`Label`}
            options={[{ value: 1, label: "1" }]}
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
