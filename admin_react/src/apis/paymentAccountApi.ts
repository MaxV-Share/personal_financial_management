import { IBasePaging } from "src/models/Bases";
import {
  IPaymentAccountCreateOrUpdateModel,
  IPaymentAccountModel,
} from "src/models/PaymentAccount";
import { ConvertObjectToFormData } from "src/utils";
import { IFilterBodyRequest } from "../models/Bases/IFilterBodyRequest";
import axiosClient, { contentTypeFormData } from "./axiosClient";

const paymentAccountApi = {
  create(
    data: IPaymentAccountCreateOrUpdateModel
  ): Promise<IPaymentAccountModel> {
    const url = "/PaymentAccounts";
    const formData = ConvertObjectToFormData(data, new FormData());
    return axiosClient(contentTypeFormData).post(url, formData);
  },
  update(
    data: IPaymentAccountCreateOrUpdateModel
  ): Promise<IPaymentAccountModel> {
    const url = `/PaymentAccounts/${data.id}`;
    const formData = ConvertObjectToFormData(data, new FormData());
    return axiosClient(contentTypeFormData).put(url, formData);
  },
  delete(id: string): Promise<any> {
    const url = `/PaymentAccounts/${id}`;
    return axiosClient().delete(url);
  },
  getAll(data: IFilterBodyRequest): Promise<IBasePaging<IPaymentAccountModel>> {
    const url = "/paymentAccounts/filter";
    return axiosClient().post(url, data);
  },
  getById(id: string): Promise<IPaymentAccountModel> {
    const url = `/PaymentAccounts/${id}`;
    return axiosClient().get(url);
  },
};

export default paymentAccountApi;
