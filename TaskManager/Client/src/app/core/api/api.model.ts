export enum ResponseStatusType {
    SUCCEED = 'Succeed', WARNING = 'Warning', ERROR = 'Error'
  }
  
  export class ResponseModel<D = null> {
    message: string;
    messageDetails: string;
    responseStatusType: ResponseStatusType;
    data: D;
  
    static isResponseModel(obj: any): obj is ResponseModel<any> {
      return obj && !!obj.data;
    }
  
  }
  