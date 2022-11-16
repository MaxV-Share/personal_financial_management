import { PayloadAction } from "@reduxjs/toolkit";
import dayjs from "dayjs";
import { put, takeLatest } from "redux-saga/effects";
import { IFilterBodyRequest } from "src/models/Bases";
import { ITransactionPerDateModelList } from "src/models/Transaction/ITransactionPerDateModelList";
import { transactionActions } from "./transactionSlice";

function* fetchTransactions(action: PayloadAction<IFilterBodyRequest>) {
  try {
    const result: ITransactionPerDateModelList = {
      data: [
        {
          date: dayjs().toISOString(),
          totalExpense: -100000,
          totalRevenue: 110000,
          transactions: [
            {
              id: "1",
              amount: 100000,
              fees: 10000,
              totalAmount: 110000,
              description: "description",
              transactionDate: dayjs().toDate().toISOString(),
            },
            {
              id: "2",
              amount: -100000,
              fees: 0,
              totalAmount: -100000,
              description: "description",
              transactionDate: dayjs().toDate().toISOString(),
            },
          ],
        },
        {
          date: dayjs().add(-1, "d").toDate().toISOString(),
          totalExpense: 0,
          totalRevenue: 960000,
          transactions: [
            {
              id: "1",
              amount: 900000,
              fees: 10000,
              totalAmount: 910000,
              description: "description",
              transactionDate: dayjs().add(-1, "d").toDate().toISOString(),
            },
            {
              id: "2",
              amount: 50000,
              fees: 0,
              totalAmount: 50000,
              description: "description",
              transactionDate: dayjs().add(-1, "d").toDate().toISOString(),
            },
          ],
        },
      ],
      pagination: {
        pageIndex: 1,
        pagesCount: 10,
        pageSize: 10,
        totalRows: 100,
      },
    };
    yield put(transactionActions.fetchTransactionsSuccess(result));
  } catch (error) {
    console.error("error", error);
  }
}

export default function* transactionSaga() {
  yield takeLatest(transactionActions.fetchTransactions, fetchTransactions);
}
