import { MessageTypeEnum } from "./message-type-enum";

export interface Message {
  code: number;
  messageType: MessageTypeEnum;
  text: string;
}


