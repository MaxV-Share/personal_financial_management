import { PayloadAction } from "@reduxjs/toolkit";
import { call, put, takeLatest } from "redux-saga/effects";
import currencyApi from "src/apis/currencyApi";
import { IBasePaging, IFilterBodyRequest } from "src/models/Bases";
import { ICurrencyModel } from "src/models/Currency";
import { ICurrencyCreateOrUpdateRequest } from "src/models/Currency/Request/ICurrencyCreateOrUpdateRequest";
import { currencyActions } from "./currencySlice";

function* fetchCurrencies(action: PayloadAction<IFilterBodyRequest>) {
  try {
    const res: IBasePaging<ICurrencyModel> = yield call(
      currencyApi.getAll,
      action.payload
    );
    yield put(currencyActions.fetchCurrenciesSuccess(res));
  } catch (error) {
    console.error("error", error);
  }
}
function* createCurrency(
  action: PayloadAction<ICurrencyCreateOrUpdateRequest>
) {}

export default function* currencySaga() {
  yield takeLatest(currencyActions.fetchCurrencies, fetchCurrencies);
  yield takeLatest(currencyActions.createCurrency, createCurrency);
}
