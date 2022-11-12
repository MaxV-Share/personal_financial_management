import { PayloadAction } from "@reduxjs/toolkit";
import dayjs from "dayjs";
import { put, takeLatest } from "redux-saga/effects";
import { IBasePaging, IFilterBodyRequest } from "src/models/Bases";
import { IPaymentAccountModel } from "src/models/PaymentAccount";
import { IFetchTransactionsByPaymentAccountRequest } from "src/models/PaymentAccount/Requests/IFetchTransactionsRequest";
import { ITransactionPerDateModelList } from "src/models/Transaction/ITransactionPerDateModelList";
import { paymentAccountActions } from "./paymentAccountSlice";

function* fetchPaymentAccounts(action: PayloadAction<IFilterBodyRequest>) {
  try {
    // const res: IBasePaging<IPaymentAccountModel> = yield call(
    //   paymentAccountApi.getAll,
    //   action.payload
    // );
    const res: IBasePaging<IPaymentAccountModel> = {
      data: [
        {
          id: "id",
          name: "tiền mặt",
          isReport: false,
          description: "Description",
          initialMoney: 1000000,
          type: {
            id: "id",
            code: "code",
            name: "type Name",
          },
          currency: {
            code: "VND",
            name: "name",
            icon: "",
            id: "id",
          },
        },
        {
          id: "id1",
          name: "name",
          isReport: false,
          description: "Description",
          initialMoney: 10000000,
          type: {
            id: "id",
            code: "code",
            name: "type Name",
          },
          currency: {
            code: "VND",
            name: "name",
            icon: "",
            id: "id",
          },
        },
      ],
      pagination: {
        pageIndex: 1,
        pagesCount: 10,
        pageSize: 10,
        totalRows: 100,
      },
    };
    yield put(paymentAccountActions.fetchPaymentAccountsSuccess(res));
  } catch (error) {
    // toast
    console.error("error", error);
  }
}
function* fetchTransactionsByPaymentAccount(
  action: PayloadAction<IFetchTransactionsByPaymentAccountRequest>
) {
  try {
    // fetch api here
    const result: ITransactionPerDateModelList = {
      data: [
        {
          date: dayjs().toDate(),
          totalExpense: -100000,
          totalRevenue: 110000,
          transactions: [
            {
              id: "1",
              amount: 100000,
              fees: 10000,
              totalAmount: 110000,
              description: "description",
              transactionDate: dayjs().toDate(),
            },
            {
              id: "2",
              amount: -100000,
              fees: 0,
              totalAmount: -100000,
              description: "description",
              transactionDate: dayjs().toDate(),
            },
          ],
        },
        {
          date: dayjs().add(-1, "d").toDate(),
          totalExpense: 0,
          totalRevenue: 960000,
          transactions: [
            {
              id: "1",
              amount: 900000,
              fees: 10000,
              totalAmount: 910000,
              description: "description",
              transactionDate: dayjs().add(-1, "d").toDate(),
            },
            {
              id: "2",
              amount: 50000,
              fees: 0,
              totalAmount: 50000,
              description: "description",
              transactionDate: dayjs().add(-1, "d").toDate(),
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
    console.log("paymentAccountSaga.fetchTransactionsByPaymentAccount", result);
    yield put(
      paymentAccountActions.fetchTransactionsByPaymentAccountSuccess(result)
    );
  } catch (error) {}
}

export default function* paymentAccountSaga() {
  yield takeLatest(
    paymentAccountActions.fetchPaymentAccounts,
    fetchPaymentAccounts
  );
  yield takeLatest(
    paymentAccountActions.fetchTransactionsByPaymentAccount,
    fetchTransactionsByPaymentAccount
  );
}
