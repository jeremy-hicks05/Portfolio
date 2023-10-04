import { ApprovalState } from './../approval-states/approval-state';
import { Category } from './../categories/category';
import { Impact } from './../impacts/impact';
import { TicketSubType } from './../ticket-sub-types/ticket-sub-type';

export interface Ticket {
  ticketId: number;
  categoryId: number;
  subTypeId: number;
  impactId: number;
  summary: string;
  reasonForRejection?: string;
  approvalStateId: number;
  approvedBy?: string;
  dateEntered: Date;
  dateLastUpdated: Date;
  enteredByUser: string;
  approvalState?: ApprovalState;
  category?: Category;
  impact?: Impact;
  subType?: TicketSubType;
}
