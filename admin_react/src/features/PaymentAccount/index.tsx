import AddTwoToneIcon from "@mui/icons-material/AddTwoTone";
import { Button, Container, Grid, Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { useAppDispatch, useAppSelector } from "src/app/hooks";
import PageTitleWrapper from "src/components/PageTitleWrapper";
import PaymentAccountList from "./Components/PaymentAccountList";
import {
  paymentAccountActions,
  selectFilterPaymentAccountRequest,
} from "./paymentAccountSlice";

export interface IPaymentAccountProps {}

export default function PaymentAccount(props: IPaymentAccountProps) {
  const filter = useAppSelector(selectFilterPaymentAccountRequest);
  const dispatch = useAppDispatch();
  useEffect(() => {
    dispatch(paymentAccountActions.fetchPaymentAccounts(filter));
  }, [dispatch, filter]);
  const navigate = useNavigate();
  return (
    <>
      <PageTitleWrapper>
        <Grid container justifyContent="space-between" alignItems="center">
          <Grid item>
            <Typography variant="h3" component="h3" gutterBottom>
              PaymentAccount header
            </Typography>
          </Grid>
          <Grid item>
            <Button
              sx={{ mt: { xs: 2, md: 0 } }}
              variant="outlined"
              startIcon={<AddTwoToneIcon fontSize="small" />}
              onClick={() => navigate(`add`)}
            >
              Create PaymentAccount
            </Button>
          </Grid>
        </Grid>
      </PageTitleWrapper>
      <Container maxWidth="lg">
        <PaymentAccountList />
      </Container>
    </>
  );
}
