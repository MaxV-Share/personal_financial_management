import { yupResolver } from "@hookform/resolvers/yup";
import { Box, Button, Container, Grid, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router";
import { useParams } from "react-router-dom";
import { InputField } from "src/components/FormFields/InputField";
import { SelectField } from "src/components/FormFields/SelectField";
import PageTitleWrapper from "src/components/PageTitleWrapper";
import { IBaseAddOrUpdateBodyRequest } from "src/models/Bases";
import * as yup from "yup";
export interface ITransactionCategoryAddOrUpdateProps {}
export type ITransactionCategoryAddOrUpdateParams = {
  id?: string;
};
const schema = yup.object().shape({
  data: yup.object().shape({
    code: yup.string().required("Please enter code."),
    name: yup.string().required("Please enter name."),
    icon: yup.string().required("Please enter icon."),
  }),
});

export default function TransactionCategoryAddOrUpdate(
  props: ITransactionCategoryAddOrUpdateProps
) {
  const { id } = useParams<ITransactionCategoryAddOrUpdateParams>();
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
      <PageTitleWrapper>
        <Grid container justifyContent="space-between" alignItems="center">
          <Grid item>
            <Typography variant="h3" component="h3" gutterBottom>
              TransactionCategoryAddOrUpdate {id}
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
            label={`TransactionCategory code`}
          />
          <InputField
            id={`data.name`}
            name={`data.name`}
            control={control}
            label={`TransactionCategory name`}
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
