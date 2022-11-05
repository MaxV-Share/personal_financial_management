import AddTwoToneIcon from "@mui/icons-material/AddTwoTone";
import { Button, Container, Grid, Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { useAppDispatch, useAppSelector } from "src/app/hooks";
import PageTitleWrapper from "src/components/PageTitleWrapper";
import PaymentAccountTypeTable from "./Components/PaymentAccountTypeTable";
import {
  paymentAccountTypeActions,
  selectFilterPaymentAccountTypeRequest,
} from "./paymentAccountTypeSlice";

export interface IPaymentAccountTypeProps {}

export default function PaymentAccountType(props: IPaymentAccountTypeProps) {
  const filter = useAppSelector(selectFilterPaymentAccountTypeRequest);
  const dispatch = useAppDispatch();
  useEffect(() => {
    dispatch(paymentAccountTypeActions.fetchPaymentAccountTypes(filter));
  }, [dispatch, filter]);
  const navigate = useNavigate();
  return (
    <>
      <PageTitleWrapper>
        <Grid container justifyContent="space-between" alignItems="center">
          <Grid item>
            <Typography variant="h3" component="h3" gutterBottom>
              PaymentAccountType header
            </Typography>
          </Grid>
          <Grid item>
            <Button
              sx={{ mt: { xs: 2, md: 0 } }}
              variant="outlined"
              startIcon={<AddTwoToneIcon fontSize="small" />}
              onClick={() => navigate(`add`)}
            >
              Create PaymentAccountType
            </Button>
          </Grid>
        </Grid>
      </PageTitleWrapper>
      <Container maxWidth="lg">
        <PaymentAccountTypeTable />
      </Container>
    </>
  );
}
