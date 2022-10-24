import { Button, Container, Grid } from "@mui/material";
import { useNavigate } from "react-router";
import PaymentAccountTypeTable from "./Components/PaymentAccountTypeTable";

export interface IPaymentAccountTypeProps {}

export default function PaymentAccountType(props: IPaymentAccountTypeProps) {
  const navigate = useNavigate();
  return (
    <div>
      <Container maxWidth="lg">
        <Grid
          container
          direction="row"
          justifyContent="center"
          alignItems="stretch"
          spacing={3}
        >
          <Grid item xs={12}>
            <Button
              variant="outlined"
              sx={{ margin: 1 }}
              onClick={() => navigate("add-or-update/1")}
            >
              Add
            </Button>
            <PaymentAccountTypeTable />
          </Grid>
        </Grid>
      </Container>
    </div>
  );
}
