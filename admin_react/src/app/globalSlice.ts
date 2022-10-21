import { createSlice } from '@reduxjs/toolkit';

export interface GlobalState {}
const initialState: GlobalState = {};

const globalSlice = createSlice({
  name: 'global',
  initialState,
  reducers: {
    // fetchCategories(state, action: PayloadAction<any>) {
    // },
  }
});
// Actions
export const categoryActions = globalSlice.actions;

// Reducer
const globalReducer = globalSlice.reducer;
export default globalReducer;

// Selectors
