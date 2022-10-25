import { yupResolver } from "@hookform/resolvers/yup";
import { Box, Button, Container, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { InputField } from "src/components/FormFields/InputField";
import { IBaseAddOrUpdateBodyRequest } from "src/models/Bases";
import { IPaymentAccountCreateOrUpdateModel } from "src/models/PaymentAccount/IPaymentAccountCreateOrUpdateModel";
import * as yup from "yup";
export interface IPaymentAccountAddOrUpdateProps {}
export type IPaymentAccountAddOrUpdateParams = {
  id?: string;
};
const schema = yup.object({
  data: yup.object().shape({
    name: yup.string().required("Please enter name."),
    icon: yup.boolean().required("Please enter icon."),
    initialMoney: yup.string().required("Please enter initial money."),
  }),
});

export default function PaymentAccountAddOrUpdate(
  props: IPaymentAccountAddOrUpdateProps
) {
  const { id } = useParams<IPaymentAccountAddOrUpdateParams>();
  const navigate = useNavigate();
  const {
    control,
    handleSubmit,
    register,
    reset,
    formState: { isSubmitting, errors },
  } = useForm<
    IBaseAddOrUpdateBodyRequest<IPaymentAccountCreateOrUpdateModel>,
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
  const onSubmit = (object) => {
    console.log("PaymentAccountAddOrUpdate", object);
    toast.success("Save PaymentAccountAddOrUpdate successfully!");
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
            name={`data.initialMoney`}
            control={control}
            label={`Initial Money`}
            type="number"
          />
          <InputField
            name={`data.name`}
            control={control}
            label={`Currency name`}
          />
          <InputField
            name={`data.isReport`}
            control={control}
            label={`isReport`}
          />
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
