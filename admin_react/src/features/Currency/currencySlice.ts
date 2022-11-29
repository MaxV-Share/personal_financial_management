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
import { ICurrencyModel } from "src/models/Currency";

export interface ICurrencyTable {
  data: ICurrencyModel[];
  pagination: IPagination;
}

export interface CurrencyState {
  status: IStatus;
  table: IBaseLoading & ICurrencyTable;
  filterCurrencyRequest: IFilterBodyRequest;
  langFilterRequest: IFilterBodyRequest;
}
const initialPagination: IPagination = {
  pageIndex: 1,
  pageSize: 5,
  pagesCount: 0,
  totalRows: 0,
};
const initialState: CurrencyState = {
  status: IStatus.None,
  table: {
    status: IStatus.None,
    data: [],
    pagination: initialPagination,
  },
  filterCurrencyRequest: {
    langId: "EN",
    pagination: initialPagination,
  },
  langFilterRequest: {
    langId: "",
    pagination: initialPagination,
  },
};

const currencySlice = createSlice({
  name: "currency",
  initialState,
  reducers: {
    fetchCurrencies(state, action: PayloadAction<IFilterBodyRequest>) {
      state.status = IStatus.Pending;
    },
    fetchCurrenciesSuccess(
      state,
      action: PayloadAction<IBasePaging<ICurrencyModel>>
    ) {
      state.table.data = action.payload.data;
      state.table.pagination = action.payload.pagination;
      state.table.status = IStatus.Success;
    },
    resetFilter(state) {
      state.filterCurrencyRequest = {
        langId: "EN",
        pagination: initialPagination,
      };
    },
    setFilter(state, action: PayloadAction<IKeyValue>) {
      if (
        get(state.filterCurrencyRequest, action.payload.key) !=
        action.payload.value
      ) {
        set(
          state.filterCurrencyRequest,
          action.payload.key,
          action.payload.value
        );
        state.table.status = IStatus.Pending;
      }
    },
  },
});
// Actions
export const currencyActions = currencySlice.actions;

// Reducer
const currencyReducer = currencySlice.reducer;
export default currencyReducer;

// Selectors
export const selectCurrencyTable = createSelector(
  [
    (state: RootState) => state.currency.table,
    (state: RootState) => state.currency.table.data,
    (state: RootState) => state.currency.table.status,
    (state: RootState) => state.currency.table.pagination,
  ],
  (table) => {
    return table;
  }
);
export const selectCurrencyTableLoading = (state: RootState) =>
  state.currency.status;
export const selectCurrencyTablePagination = (state: RootState) =>
  state.currency.table.pagination;
export const selectFilterCurrencyRequest = (state: RootState) =>
  state.currency.filterCurrencyRequest;
