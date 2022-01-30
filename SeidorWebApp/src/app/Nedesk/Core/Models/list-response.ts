import { BaseResponse } from "./base-response";

export class ListResponse<T> extends BaseResponse<T[]>  {
  itemsPerPage: number = 0;
  totalPages: number = 0;
  pageIndex: number = 0;
  totalItems: number = 0;
}
