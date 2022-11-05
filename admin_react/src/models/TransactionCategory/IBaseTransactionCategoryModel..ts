import { IBaseModel } from "../Bases";
export interface IBaseTransactionCategoryModel extends IBaseModel<string> {
  name?: string;
  description?: string;
  icon?: string;
  code?: string;
}
