import { PayloadAction } from '@reduxjs/toolkit';
import { put, takeLatest } from 'redux-saga/effects';
import { IBasePaging, IFilterBodyRequest } from 'src/models/Bases';
import { ICurrencyModel } from 'src/models/Currency';
import { currencyActions } from './currencySlice';

function* fetchCurrencies(action: PayloadAction<IFilterBodyRequest>) {
  try {
    // const res: IBasePaging<ICurrencyModel> = yield call(
    //   currencyApi.getAll,
    //   action.payload
    // );
    const res: IBasePaging<ICurrencyModel> = {
      data: [
        {
          id: 'id',
          name: 'name',
          icon: 'symbol',
          code: 'code'
        },
        {
          id: 'id2',
          name: 'name2',
          icon: 'symbol2',
          code: 'code2'
        }
      ],
      pagination: {
        pageIndex: 1,
        pagesCount: 10,
        pageSize: 10,
        totalRows: 100
      }
    };
    yield put(currencyActions.fetchCurrenciesSuccess(res));
  } catch (error) {
    console.error('error', error);
  }
}

export default function* currencySaga() {
  yield takeLatest(currencyActions.fetchCurrencies, fetchCurrencies);
}
