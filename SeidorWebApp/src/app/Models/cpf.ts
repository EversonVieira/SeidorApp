import { BaseModel } from "../Nedesk/Core/Models/base-model";

export class CPF extends BaseModel {
    ownerName:string = '';
    document:string = '';
    isBlocked:boolean = false;
    lastUse:Date = new Date(new Date().toUTCString())
}
