import { Button, Container, Grid } from "@mui/material";
import moment from "moment";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "src/app/hooks";
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
  const yesterday = moment().subtract(1, "day");
  console.log(yesterday);
  return (
    <>
      {/* <Helmet>
        <title>Currency</title>
      </Helmet> */}
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
            <CurrencyTable></CurrencyTable>
          </Grid>
        </Grid>
      </Container>
    </>
  );
}
