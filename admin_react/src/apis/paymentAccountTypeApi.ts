import { IBasePaging } from "src/models/Bases";
import { IPaymentAccountTypeModel } from "src/models/PaymentAccountType";
import { ConvertObjectToFormData } from "src/utils";
import { IFilterBodyRequest } from "../models/Bases/IFilterBodyRequest";
import { IPaymentAccountTypeCreateOrUpdateModel } from "../models/PaymentAccountType/Requests/IPaymentAccountTypeCreateOrUpdateModel";
import axiosClient, { contentTypeFormData } from "./axiosClient";

const paymentAccountApi = {
  create(
    data: IPaymentAccountTypeCreateOrUpdateModel
  ): Promise<IPaymentAccountTypeModel> {
    const url = "/PaymentAccountTypes";
    const formData = ConvertObjectToFormData(data, new FormData());
    return axiosClient(contentTypeFormData).post(url, formData);
  },
  update(
    data: IPaymentAccountTypeCreateOrUpdateModel
  ): Promise<IPaymentAccountTypeModel> {
    const url = `/PaymentAccountTypes/${data.id}`;
    const formData = ConvertObjectToFormData(data, new FormData());
    return axiosClient(contentTypeFormData).put(url, formData);
  },
  delete(id: string): Promise<any> {
    const url = `/PaymentAccountTypes/${id}`;
    return axiosClient().delete(url);
  },
  getAll(
    data: IFilterBodyRequest
  ): Promise<IBasePaging<IPaymentAccountTypeModel>> {
    const url = "/PaymentAccountTypes/filter";
    return axiosClient().post(url, data);
  },
  getById(id: string): Promise<IPaymentAccountTypeModel> {
    const url = `/PaymentAccountTypes/${id}`;
    return axiosClient().get(url);
  },
};

export default paymentAccountApi;
