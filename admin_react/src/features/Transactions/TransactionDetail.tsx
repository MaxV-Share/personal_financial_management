export interface ITransactionDetailProps {}
import { Container, Grid, Typography } from '@mui/material';
import PageTitleWrapper from 'src/components/PageTitleWrapper';

export default function TransactionDetail(props: ITransactionDetailProps) {
  return <>
      <PageTitleWrapper>
        <Grid container justifyContent="space-between" alignItems="center">
          <Grid item>
            <Typography variant="h4" component="h3" gutterBottom>
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
    <Container maxWidth="lg">TransactionDetail</Container>
  </>;
}
