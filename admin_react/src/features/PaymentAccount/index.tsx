import { Container, Grid } from "@mui/material";
import { useNavigate } from "react-router";
import PaymentAccountList from "./Components/PaymentAccountList";

export interface IPaymentAccountProps {}

export default function PaymentAccount(props: IPaymentAccountProps) {
  const navigate = useNavigate();
  return (
    <>
      <Container maxWidth="lg">
        <Grid
          container
          direction="row"
          justifyContent="center"
          alignItems="stretch"
          spacing={3}
          sx={{ mt: 1 }}
        >
          <Grid item xs={12}>
            <PaymentAccountList />
          </Grid>
        </Grid>
      </Container>
    </>
  );
}
