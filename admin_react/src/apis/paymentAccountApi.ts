import { IBasePaging } from "src/models/Bases";
import { IPaymentAccountModel } from "src/models/PaymentAccount";
import { IFilterBodyRequest } from "../models/Bases/IFilterBodyRequest";
import axiosClient from "./axiosClient";

const paymentAccountApi = {
  getAll(data: IFilterBodyRequest): Promise<IBasePaging<IPaymentAccountModel>> {
    const url = "/paymentAccounts/filter";
    return axiosClient().post(url, data);
  },
};

export default paymentAccountApi;
