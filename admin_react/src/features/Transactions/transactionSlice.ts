import { createSelector, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { RootState } from "src/app/store";
import {
  IBaseLoading,
  IFilterBodyRequest,
  IPagination,
} from "src/models/Bases";
import { IStatus } from "src/models/Common/IStatus";
import { ITransactionPerDateModelList } from "src/models/Transaction/ITransactionPerDateModelList";

export interface TransactionState {
  status: IStatus;
  transactionPerDateList: IBaseLoading & ITransactionPerDateModelList;
  filterTransactionRequest: IFilterBodyRequest;
  langFilterRequest: IFilterBodyRequest;
}
const initialPagination: IPagination = {
  pageIndex: 1,
  pageSize: 5,
  pagesCount: 0,
  totalRows: 0,
};
const initialState: TransactionState = {
  status: IStatus.None,
  transactionPerDateList: {
    status: IStatus.None,
    data: [],
    pagination: initialPagination,
  },
  filterTransactionRequest: {
    langId: "EN",
    pagination: initialPagination,
  },
  langFilterRequest: {
    langId: "",
    pagination: initialPagination,
  },
};

const transactionSlice = createSlice({
  name: "transaction",
  initialState,
  reducers: {
    fetchTransactions(state, action: PayloadAction<IFilterBodyRequest>) {
      state.status = IStatus.Pending;
      state.transactionPerDateList.status = IStatus.Pending;
    },
    fetchTransactionsSuccess(
      state,
      action: PayloadAction<ITransactionPerDateModelList>
    ) {
      state.status = IStatus.Success;
      state.transactionPerDateList.status = IStatus.Success;
      state.transactionPerDateList.data = action.payload.data;
      state.transactionPerDateList.pagination = action.payload.pagination;
    },
  },
});
// Actions
export const transactionActions = transactionSlice.actions;

// Reducer
const transactionReducer = transactionSlice.reducer;
export default transactionReducer;

// Selectors
export const selectTransactionList = createSelector(
  [
    (state: RootState) => state.transaction.transactionPerDateList,
    (state: RootState) => state.transaction.transactionPerDateList.data,
    (state: RootState) => state.transaction.transactionPerDateList.status,
    (state: RootState) => state.transaction.transactionPerDateList.pagination,
  ],
  (transactionPerDateList) => {
    return transactionPerDateList;
  }
);
export const selectTransactionsLoading = (state: RootState) =>
  state.transaction.status;
export const selectTransactionsPagination = (state: RootState) =>
  state.transaction.transactionPerDateList.pagination;
export const selectFilterTransactionRequest = (state: RootState) =>
  state.transaction.filterTransactionRequest;
