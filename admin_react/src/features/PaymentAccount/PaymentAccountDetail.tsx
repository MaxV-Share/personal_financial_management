import { Card, Container, Grid, Typography } from "@mui/material";
import { Box } from "@mui/system";
// import { DateRange } from "@mui/x-date-pickers";
import PageTitleWrapper from "src/components/PageTitleWrapper";
export interface IPaymentAccountDetailProps {}

export default function PaymentAccountDetail(
  props: IPaymentAccountDetailProps
) {
  // const [value, setValue] = React.useState<DateRange<Dayjs>>([null, null]);

  return (
    <>
      <PageTitleWrapper>
        <Grid container justifyContent="space-between" alignItems="center">
          <Grid item>
            <Typography variant="h3" component="h3" gutterBottom>
              PaymentAccountDetail header
            </Typography>
          </Grid>
          <Grid item>
            {/* <Button
              sx={{ mt: { xs: 2, md: 0 } }}
              variant="contained"
              startIcon={<AddTwoToneIcon fontSize="small" />}
            >
              Create transaction
            </Button> */}
          </Grid>
        </Grid>
      </PageTitleWrapper>
      <Container maxWidth="lg">
        <Card sx={{ p: 1 }}></Card>
      </Container>
      <Box>ss e</Box>
    </>
  );
}
