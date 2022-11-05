import { Button, Card, Container, Grid, Typography } from "@mui/material";
import { LocalizationProvider, MobileDatePicker } from "@mui/x-date-pickers";
// import { DateRange } from "@mui/x-date-pickers";
import AddTwoToneIcon from "@mui/icons-material/AddTwoTone";
import TextField from "@mui/material/TextField";
import { Box } from "@mui/system";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import dayjs, { Dayjs } from "dayjs";
import { useState } from "react";
import PageTitleWrapper from "src/components/PageTitleWrapper";
import TransactionDateList from "../Transactions/Components/TransactionDateList";
export interface IPaymentAccountDetailProps {}

export default function PaymentAccountDetail(
  props: IPaymentAccountDetailProps
) {
  const [fromDate, setFromDate] = useState<Dayjs | null>(
    dayjs()
      .add(-1, "M")
      .add(+1, "d")
  );
  const [toDate, setToDate] = useState<Dayjs | null>(dayjs());

  const handleChange = (newValue: Dayjs | null) => {
    //setValue(newValue);
  };

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
            <Button
              sx={{ mt: { xs: 2, md: 0 } }}
              variant="outlined"
              startIcon={<AddTwoToneIcon fontSize="small" />}
            >
              Create transaction
            </Button>
          </Grid>
        </Grid>
      </PageTitleWrapper>
      <Container maxWidth="lg">
        <Card sx={{ p: 2 }}>
          <Grid display="flex" justifyContent="center" alignItems="center">
            <Box mx={1}>
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <MobileDatePicker
                  label="From"
                  inputFormat="DD/MM/YYYY"
                  views={["year", "month"]}
                  value={fromDate}
                  onChange={handleChange}
                  renderInput={(params) => <TextField {...params} />}
                />
              </LocalizationProvider>
            </Box>
            <Box mx={1}>
              <LocalizationProvider dateAdapter={AdapterDayjs} mx={1}>
                <MobileDatePicker
                  label="To"
                  inputFormat="DD/MM/YYYY"
                  value={toDate}
                  onChange={handleChange}
                  renderInput={(params) => <TextField {...params} />}
                />
              </LocalizationProvider>
            </Box>
            <Box>
              <Button
                variant="outlined"
                // sx={{ margin: 1 }}
                // onClick={() => {}}
              >
                Search
              </Button>
            </Box>
          </Grid>
        </Card>
        <Box pt={2}>
          <TransactionDateList data={[]} />
        </Box>
      </Container>
    </>
  );
}
