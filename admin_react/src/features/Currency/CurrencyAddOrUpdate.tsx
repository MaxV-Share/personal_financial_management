import { yupResolver } from "@hookform/resolvers/yup";
import { Box, Button, Container, Grid, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import { useAppDispatch } from "src/app/hooks";
import { InputField } from "src/components/FormFields/InputField";
import PageTitleWrapper from "src/components/PageTitleWrapper";
import { IBaseAddOrUpdateBodyRequest } from "src/models/Bases";
import * as yup from "yup";
import { currencyActions } from "./currencySlice";
export interface ICurrencyAddOrUpdateProps {}
export type ICurrencyAddOrUpdateParams = {
  id?: string;
};
const schema = yup.object().shape({
  data: yup.object().shape({
    code: yup.string().required("Please enter code."),
    name: yup.string().required("Please enter name."),
    icon: yup.string().required("Please enter icon."),
  }),
});

export default function CurrencyAddOrUpdate(props: ICurrencyAddOrUpdateProps) {
  const { id } = useParams<ICurrencyAddOrUpdateParams>();
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
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
    dispatch(currencyActions.createCurrency(object.data));
    console.log(object);
  };
  return (
    <>
      <PageTitleWrapper>
        <Grid container justifyContent="space-between" alignItems="center">
          <Grid item>
            <Typography variant="h3" component="h3" gutterBottom>
              CurrencyAddOrUpdate {id}
            </Typography>
          </Grid>
        </Grid>
      </PageTitleWrapper>
      <Container maxWidth="lg">
        <form onSubmit={handleSubmit(onSubmit)}>
          <InputField
            id={`data.code`}
            name={`data.code`}
            control={control}
            label={`Currency code`}
          />
          <InputField
            id={`data.name`}
            name={`data.name`}
            control={control}
            label={`Currency name`}
          />
          <InputField
            id={`data.icon`}
            name={`data.icon`}
            control={control}
            label={`Currency icon`}
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
