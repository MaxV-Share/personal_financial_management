import AddTwoToneIcon from "@mui/icons-material/AddTwoTone";
import { Button, Container, Grid, Typography } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "src/app/hooks";
import PageTitleWrapper from "src/components/PageTitleWrapper";
import CurrencyTable from "./Components/CurrencyTable";
import { currencyActions, selectFilterCurrencyRequest } from "./currencySlice";

export interface ICurrencyProps {}

export default function Currency(props: ICurrencyProps) {
  const filter = useAppSelector(selectFilterCurrencyRequest);
  const dispatch = useAppDispatch();
  useEffect(() => {
    dispatch(currencyActions.fetchCurrencies(filter));
  }, [dispatch, filter]);
  const navigate = useNavigate();
  return (
    <>
      {/* <Helmet>
        <title>Currency</title>
      </Helmet> */}
      <PageTitleWrapper>
        <Grid container justifyContent="space-between" alignItems="center">
          <Grid item>
            <Typography variant="h3" component="h3" gutterBottom>
              Currency header
            </Typography>
          </Grid>
          <Grid item>
            <Button
              sx={{ mt: { xs: 2, md: 0 } }}
              variant="outlined"
              startIcon={<AddTwoToneIcon fontSize="small" />}
              onClick={() => navigate(`add`)}
            >
              Create Currency
            </Button>
          </Grid>
        </Grid>
      </PageTitleWrapper>
      <Container maxWidth="lg">
        <Grid
          container
          direction="row"
          justifyContent="center"
          alignItems="stretch"
          spacing={3}
        >
          <Grid item xs={12}>
            <CurrencyTable />
          </Grid>
        </Grid>
      </Container>
    </>
  );
}
