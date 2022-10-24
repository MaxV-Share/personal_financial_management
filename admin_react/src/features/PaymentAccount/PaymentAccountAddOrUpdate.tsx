import { yupResolver } from "@hookform/resolvers/yup";
import ArrowBackTwoToneIcon from "@mui/icons-material/ArrowBackTwoTone";
import {
  Box,
  Button,
  Container,
  IconButton,
  Tooltip,
  Typography,
} from "@mui/material";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import { InputField } from "src/components/FormFields/InputField";
import { SelectField } from "src/components/FormFields/SelectField";
import { IBaseAddOrUpdateBodyRequest } from "src/models/Bases";
import * as yup from "yup";
export interface IPaymentAccountAddOrUpdateProps {}
export type IPaymentAccountAddOrUpdateParams = {
  id?: string;
};
const schema = yup.object().shape({
  data: yup.object().shape({
    code: yup.string().required("Please enter code."),
    name: yup.string().required("Please enter name."),
    icon: yup.string().required("Please enter icon."),
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
  } = useForm<IBaseAddOrUpdateBodyRequest<any>, object>({
    defaultValues: {
      data: {
        code: "",
      },
    },
    resolver: yupResolver(schema),
  });
  const onSubmit = (object) => {
    console.log(object);
  };
  return (
    <>
      <Container maxWidth="lg">
        <Box display="flex" mb={3}>
          <Tooltip arrow placement="top" title="Go back">
            <IconButton
              color="primary"
              sx={{ p: 1, mr: 1 }}
              size={"small"}
              onClick={() => navigate(-1)}
            >
              <ArrowBackTwoToneIcon />
            </IconButton>
          </Tooltip>
          <Box alignItems="center" justifyContent="center">
            <Typography
              variant="h3"
              component="h3"
              gutterBottom
              align="justify"
            >
              CurrencyAddOrUpdate {id}
            </Typography>
          </Box>
        </Box>
        <form onSubmit={handleSubmit(onSubmit)}>
          <InputField
            name={`data.code`}
            control={control}
            label={`Category code`}
          />
          <InputField
            name={`data.name`}
            control={control}
            label={`Currency name`}
          />
          <SelectField
            name={`data.icon`}
            control={control}
            label={`Label`}
            options={[{ value: 1, label: "1" }]}
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
