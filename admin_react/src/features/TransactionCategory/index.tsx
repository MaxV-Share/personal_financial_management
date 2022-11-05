import AddTwoToneIcon from "@mui/icons-material/AddTwoTone";
import { Button, Container, Grid, Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { useAppDispatch, useAppSelector } from "src/app/hooks";
import PageTitleWrapper from "src/components/PageTitleWrapper";
import TransactionCategoryTable from "./Components/TransactionCategoryTable";
import {
  selectFilterTransactionCategoryRequest,
  transactionCategoryActions,
} from "./transactionCategorySlice";
export interface ITransactionCategoryProps {}

export default function TransactionCategory(props: ITransactionCategoryProps) {
  const filter = useAppSelector(selectFilterTransactionCategoryRequest);
  const dispatch = useAppDispatch();
  useEffect(() => {
    dispatch(transactionCategoryActions.fetchTransactionCategories(filter));
  }, [dispatch, filter]);
  const navigate = useNavigate();
  return (
    <>
      <PageTitleWrapper>
        <Grid container justifyContent="space-between" alignItems="center">
          <Grid item>
            <Typography variant="h3" component="h3" gutterBottom>
              TransactionCategory header
            </Typography>
          </Grid>
          <Grid item>
            <Button
              sx={{ mt: { xs: 2, md: 0 } }}
              variant="outlined"
              startIcon={<AddTwoToneIcon fontSize="small" />}
              onClick={() => navigate(`add`)}
            >
              Create Transaction Category
            </Button>
          </Grid>
        </Grid>
      </PageTitleWrapper>
      <Container maxWidth="lg">
        {/* <Grid
          container
          direction="row"
          justifyContent="center"
          alignItems="stretch"
          spacing={3}
          sx={{ mt: 1 }}
        > */}
        <TransactionCategoryTable />
        {/* </Grid> */}
      </Container>
    </>
  );
}
