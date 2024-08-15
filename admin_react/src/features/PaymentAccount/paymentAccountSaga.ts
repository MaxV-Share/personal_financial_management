import { PayloadAction } from "@reduxjs/toolkit";
import dayjs from "dayjs";
import { toast } from "react-toastify";
import { call, put, takeLatest } from "redux-saga/effects";
import paymentAccountApi from "src/apis/paymentAccountApi";
import { IBasePaging, IFilterBodyRequest } from "src/models/Bases";
import {
  IPaymentAccountCreateOrUpdateModel,
  IPaymentAccountModel,
} from "src/models/PaymentAccount";
import { IFetchTransactionsByPaymentAccountRequest } from "src/models/PaymentAccount/Requests/IFetchTransactionsByPaymentAccountRequest";
import { ITransactionPerDateModelList } from "src/models/Transaction/ITransactionPerDateModelList";
import { paymentAccountActions } from "./paymentAccountSlice";

function* fetchPaymentAccounts(action: PayloadAction<IFilterBodyRequest>) {
  try {
    const res: IBasePaging<IPaymentAccountModel> = yield call(
      paymentAccountApi.getAll,
      action.payload
    );
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
          date: dayjs().toDate().toISOString(),
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
    yield put(
      paymentAccountActions.fetchTransactionsByPaymentAccountSuccess(result)
    );
  } catch (error) {}
}
function* savePaymentAccount(
  action: PayloadAction<IPaymentAccountCreateOrUpdateModel>
) {
  try {
    if (action.payload.id == null) {
      yield call(
        paymentAccountApi.create,
        action.payload as IPaymentAccountCreateOrUpdateModel
      );
    } else {
      yield call(
        paymentAccountApi.update,
        action.payload as IPaymentAccountCreateOrUpdateModel
      );
    }
    yield put(paymentAccountActions.savePaymentAccountSuccess({}));
    yield put(paymentAccountActions.fetchPaymentAccounts({}));
    toast.success(`Save PaymentAccountError Successful!`);
    // history.go(-1);
  } catch (error) {
    yield put(paymentAccountActions.savePaymentAccountError(error));
    toast.error(`Save PaymentAccountError Error!`);
  }
}

function* fetchPaymentAccount(action: PayloadAction<string>) {
  try {
    // call api get by id
    // const res: IPaymentAccountCreateOrUpdateModel =
    //   mockIPaymentAccountModel.data.find((e) => e.id == action.payload);
    const res: IPaymentAccountCreateOrUpdateModel = yield call(
      paymentAccountApi.getById,
      action.payload
    );
    if (res == null) {
      yield put(paymentAccountActions.fetchPaymentAccountError({}));
    } else {
      yield put(paymentAccountActions.fetchPaymentAccountSuccess(res));
    }
  } catch (error) {
    yield put(paymentAccountActions.fetchPaymentAccountError({}));
  }
}

function* deletePaymentAccount(action: PayloadAction<string>) {
  try {
    // call api delete payment account type
    const result = yield call(paymentAccountApi.delete, action.payload);
    yield put(paymentAccountActions.resetFilter());
  } catch (error) {
    yield put(paymentAccountActions.deletePaymentAccountError({}));
  }
}

export default function* paymentAccountSaga() {
  yield takeLatest(
    paymentAccountActions.fetchPaymentAccounts,
    fetchPaymentAccounts
  );
  yield takeLatest(
    paymentAccountActions.fetchPaymentAccount,
    fetchPaymentAccount
  );
  yield takeLatest(
    paymentAccountActions.fetchTransactionsByPaymentAccount,
    fetchTransactionsByPaymentAccount
  );
  yield takeLatest(
    paymentAccountActions.deletePaymentAccount,
    deletePaymentAccount
  );
  yield takeLatest(
    paymentAccountActions.savePaymentAccount,
    savePaymentAccount
  );
}
