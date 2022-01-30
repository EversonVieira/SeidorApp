export class BaseModel {
  id: number = 0;
  createdBy: string = '';
  createdOn: Date = new Date();
  modifiedBy: string = '';
  modifiedOn: Date = new Date();

  startCreatedDate: Date = new Date();
  endCreatedDate: Date = new Date();
  
  startModifiedDate: Date = new Date();
  endModifiedDate: Date = new Date();
}
