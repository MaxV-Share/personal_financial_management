import AddTwoToneIcon from "@mui/icons-material/AddTwoTone";
import { Button, Container, Grid, Typography } from "@mui/material";
import { useNavigate } from "react-router";
import PageTitleWrapper from "src/components/PageTitleWrapper";
import PaymentAccountList from "./Components/PaymentAccountList";

export interface IPaymentAccountProps {}

export default function PaymentAccount(props: IPaymentAccountProps) {
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
