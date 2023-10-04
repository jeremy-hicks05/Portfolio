//import { Category } from './../categories/category'

export interface TicketSubType {
  ticketSubTypeId: number;
  categoryId: number;
  //category?: Category;
  name: string;
  description: string;
  needsApproval: string;
  ccList: string;
}
