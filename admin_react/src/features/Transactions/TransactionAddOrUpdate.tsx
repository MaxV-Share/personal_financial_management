import { yupResolver } from "@hookform/resolvers/yup";
import { AddCircle } from "@mui/icons-material";
import {
  Box,
  Button,
  Checkbox,
  Container,
  FormControlLabel,
  FormGroup,
  IconButton,
  InputAdornment,
  Typography,
} from "@mui/material";
import dayjs from "dayjs";
import { Controller, useForm } from "react-hook-form";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { InputField } from "src/components/FormFields/InputField";
import PaymentAccountAutocomplete from "src/features/PaymentAccount/Components/PaymentAccountAutocomplete";
import { IBaseAddOrUpdateBodyRequest } from "src/models/Bases";
import { IPaymentAccountSelectModel } from "src/models/PaymentAccount/IPaymentAccountSelect";
import { ITransactionAddOrUpdateModel } from "src/models/Transaction/ITransactionAddOrUpdateModel";
import * as yup from "yup";
export interface ITransactionAddOrUpdateProps {}
export type ITransactionAddOrUpdateParams = {
  id?: string;
};
const schema = yup.object({
  data: yup.object().shape({
    // name: yup.string().required("Please enter name."),
    // icon: yup.boolean().required("Please enter icon."),
    // initialMoney: yup.string().required("Please enter initial money."),
  }),
});

export default function TransactionAddOrUpdate(
  props: ITransactionAddOrUpdateProps
) {
  const { id } = useParams<ITransactionAddOrUpdateParams>();
  const navigate = useNavigate();
  const {
    control,
    handleSubmit,
    register,
    reset,
    formState: { isSubmitting, errors },
    getValues,
    setValue,
  } = useForm<
    IBaseAddOrUpdateBodyRequest<ITransactionAddOrUpdateModel>,
    object
  >({
    defaultValues: {
      data: {
        amount: 0,
        totalAmount: 0,
        fees: 0,
        isReport: true,
      },
    },
    resolver: yupResolver(schema),
  });
  const onSubmit = (object) => {
    console.log("TransactionAddOrUpdate", object);
    toast.success("Save TransactionAddOrUpdate successfully!");
  };
  const optionsData: IPaymentAccountSelectModel[] = [
    {
      id: "1",
      name: "account1",
    },
    {
      id: "2",
      name: "account2",
    },
    {
      id: "3",
      name: "account3",
    },
  ];
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
              {id == null ? "Add Transaction" : `Update Transaction ${id}`}
            </Typography>
          </Box>
        </Box>
        <form onSubmit={handleSubmit(onSubmit)}>
          <InputField
            id={`data.amount`}
            name={`data.amount`}
            control={control}
            label={`amount`}
            type="number"
            endAdornment={<InputAdornment position="start">VNĐ</InputAdornment>}
            onChange={(e) => {
              const formValues = getValues();
              formValues.data.totalAmount =
                Number(formValues.data.fees) + Number(e.target.value);
              setValue("data.totalAmount", formValues.data.totalAmount);
            }}
          />
          <InputField
            id={`data.transactionDate`}
            name={`data.transactionDate`}
            control={control}
            label={`transactionDate`}
            type="datetime-local"
            defaultValue={dayjs().format("YYYY-MM-DDTHH:mm")}
          />
          <InputField
            id={`data.fees`}
            name={`data.fees`}
            control={control}
            label={`fees`}
            type="number"
            endAdornment={<InputAdornment position="start">VNĐ</InputAdornment>}
            onChange={(e) => {
              const formValues = getValues();
              formValues.data.totalAmount =
                Number(e.target.value) + Number(formValues.data.amount);
              setValue("data.totalAmount", formValues.data.totalAmount);
            }}
          />
          <InputField
            id={`data.totalAmount`}
            name={`data.totalAmount`}
            control={control}
            label={`totalAmount`}
            type="number"
            endAdornment={<InputAdornment position="start">VNĐ</InputAdornment>}
            disabled
          />
          <InputField
            id={`data.description`}
            name={`data.description`}
            control={control}
            label={`description`}
            type="text"
          />
          <PaymentAccountAutocomplete
            id={`data.fromPaymentAccount`}
            label={"from Payment Account"}
            control={control}
            name="data.fromPaymentAccount"
            optionsData={optionsData}
            startAdornment={
              <InputAdornment position="end">
                <IconButton
                  sx={{ ml: -0.5 }}
                  color="success"
                  size="small"
                  onClick={() => {}}
                >
                  <AddCircle />
                </IconButton>
              </InputAdornment>
            }
          />
          <PaymentAccountAutocomplete
            id={`data.toPaymentAccount`}
            label={"to Payment Account"}
            control={control}
            name="data.toPaymentAccount"
            optionsData={optionsData}
            startAdornment={
              <InputAdornment position="end">
                <IconButton
                  sx={{ ml: -0.5 }}
                  color="success"
                  size="small"
                  onClick={() => {}}
                >
                  <AddCircle />
                </IconButton>
              </InputAdornment>
            }
          />
          <FormGroup>
            <FormControlLabel
              control={
                <Controller
                  name={`data.isReport`}
                  control={control}
                  render={({ field: props }) => (
                    <Checkbox
                      {...props}
                      checked={props.value}
                      onChange={(e) => props.onChange(e.target.checked)}
                    />
                  )}
                />
              }
              label="isReport"
            />
          </FormGroup>
          <Box
            sx={{
              display: "flex",
              justifyContent: "center",
            }}
          >
            <Button type="submit" variant="outlined" fullWidth>
              Submit
            </Button>
          </Box>
        </form>
      </Container>
    </>
  );
}
