import { IBaseModel } from ".";

export interface IBaseViewModel<TKey> extends IBaseModel<TKey> {
  createAt?: Date,
  updateAt?: Date,
  createBy?: Date,
  updateBy?: Date,
}