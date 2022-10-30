import { IBaseModel } from "../Bases";

export interface IBaseTransactionModel extends IBaseModel<string> {
  amount?: number;
  totalAmount?: number;
  fees?: number;
  description?: string;
}
