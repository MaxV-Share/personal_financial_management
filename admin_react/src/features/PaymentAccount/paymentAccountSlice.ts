import { createSelector, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { get, set } from "lodash";
import { RootState } from "src/app/store";
import {
  IBaseLoading,
  IBasePaging,
  IFilterBodyRequest,
  IPagination,
} from "src/models/Bases";
import { IKeyValue } from "src/models/Common";
import { IStatus } from "src/models/Common/IStatus";
import { IPaymentAccountModel } from "src/models/PaymentAccount";
import { IFetchTransactionsByPaymentAccountRequest } from "src/models/PaymentAccount/Requests/IFetchTransactionsByPaymentAccountRequest";
import { ITransactionPerDateModelList } from "src/models/Transaction/ITransactionPerDateModelList";

export interface IPaymentAccountTableModel extends IBaseLoading {
  data: IPaymentAccountModel[];
  pagination: IPagination;
}

export interface PaymentAccountState {
  status: IStatus;
  table: IPaymentAccountTableModel;
  paymentAccountTransactions: ITransactionPerDateModelList & IBaseLoading;
  filterPaymentAccountRequest: IFilterBodyRequest;
  langFilterRequest: IFilterBodyRequest;
  fetchTransactionsRequest: IFetchTransactionsByPaymentAccountRequest;
}
const initialPagination: IPagination = {
  pageIndex: 1,
  pageSize: 5,
  pagesCount: 0,
  totalRows: 0,
};
const initialState: PaymentAccountState = {
  status: IStatus.None,
  table: {
    status: IStatus.None,
    data: [],
    pagination: initialPagination,
  },
  filterPaymentAccountRequest: {
    langId: "EN",
    pagination: initialPagination,
  },
  langFilterRequest: {
    langId: "",
    pagination: initialPagination,
  },
  paymentAccountTransactions: {
    status: IStatus.None,
    data: [],
    pagination: initialPagination,
  },
  fetchTransactionsRequest: {
    paymentAccountId: null,
    pagination: initialPagination,
  },
};

const paymentAccountSlice = createSlice({
  name: "paymentAccount",
  initialState,
  reducers: {
    fetchPaymentAccounts(state, action: PayloadAction<IFilterBodyRequest>) {
      state.status = IStatus.Pending;
    },
    fetchPaymentAccountsSuccess(
      state,
      action: PayloadAction<IBasePaging<IPaymentAccountModel>>
    ) {
      state.table.data = action.payload.data;
      state.table.pagination = action.payload.pagination;
      state.table.status = IStatus.Success;
    },
    fetchTransactionsByPaymentAccount(
      state,
      action: PayloadAction<IFetchTransactionsByPaymentAccountRequest>
    ) {
      state.paymentAccountTransactions.status = IStatus.Pending;
      state.fetchTransactionsRequest = action.payload;
    },
    fetchTransactionsByPaymentAccountSuccess(
      state,
      action: PayloadAction<ITransactionPerDateModelList>
    ) {
      state.paymentAccountTransactions = action.payload;
      state.paymentAccountTransactions.status = IStatus.Success;
    },
    resetFilter(state) {
      state.filterPaymentAccountRequest = {
        langId: "EN",
        pagination: initialPagination,
      };
    },
    setFilter(state, action: PayloadAction<IKeyValue>) {
      if (
        get(state.filterPaymentAccountRequest, action.payload.key) !=
        action.payload.value
      ) {
        set(
          state.filterPaymentAccountRequest,
          action.payload.key,
          action.payload.value
        );
        state.table.status = IStatus.Pending;
      }
    },
  },
});
// Actions
export const paymentAccountActions = paymentAccountSlice.actions;

// Reducer
const paymentAccountReducer = paymentAccountSlice.reducer;
export default paymentAccountReducer;

// Selectors
export const selectPaymentAccountTable = createSelector(
  [
    (state: RootState) => state.paymentAccount.table,
    (state: RootState) => state.paymentAccount.table.data,
    // (state: RootState) => state.paymentAccount.table.status,
    (state: RootState) => state.paymentAccount.table.pagination,
  ],
  (table) => {
    return table;
  }
);
export const selectPaymentAccountTableLoading = (state: RootState) =>
  state.paymentAccount.status;
export const selectPaymentAccountTablePagination = (state: RootState) =>
  state.paymentAccount.table.pagination;
export const selectFilterPaymentAccountRequest = (state: RootState) =>
  state.paymentAccount.filterPaymentAccountRequest;
export const selectPaymentAccountTransactions = createSelector(
  [(state: RootState) => state.paymentAccount.paymentAccountTransactions],
  (paymentAccountTransactions) => paymentAccountTransactions
);
