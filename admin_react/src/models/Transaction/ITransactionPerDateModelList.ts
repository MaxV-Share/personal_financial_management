import { IBasePaging } from "../Bases";
import { IBaseLoading } from "./../Bases/IBaseLoading";
import { ITransactionPerDateModel } from "./ITransactionPerDateModel";

export interface ITransactionPerDateModelList
  extends IBasePaging<ITransactionPerDateModel>,
    IBaseLoading {}
