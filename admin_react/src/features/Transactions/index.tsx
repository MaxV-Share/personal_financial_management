import AddTwoToneIcon from "@mui/icons-material/AddTwoTone";
import { Button, Container, Grid, Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "src/app/hooks";
import PageTitleWrapper from "src/components/PageTitleWrapper";
import TransactionDateList from "./Components/TransactionDateList";
import { selectTransactionList, transactionActions } from "./transactionSlice";

export interface ITransactionsProps {}

export default function Transactions(props: ITransactionsProps) {
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  useEffect(() => {
    dispatch(transactionActions.fetchTransactions({}));
  }, []);

  const transactionDateList = useAppSelector(selectTransactionList);
  return (
    <>
      <PageTitleWrapper>
        <Grid container justifyContent="space-between" alignItems="center">
          <Grid item>
            <Typography variant="h3" component="h3" gutterBottom>
              Transactions header
            </Typography>
          </Grid>
          <Grid item>
            <Button
              sx={{ mt: { xs: 2, md: 0 } }}
              variant="outlined"
              startIcon={<AddTwoToneIcon fontSize="small" />}
              onClick={() => navigate(`add`)}
            >
              Create Transaction
            </Button>
          </Grid>
        </Grid>
      </PageTitleWrapper>
      <Container maxWidth="lg">
        <TransactionDateList data={transactionDateList.data} />
      </Container>
    </>
  );
}
