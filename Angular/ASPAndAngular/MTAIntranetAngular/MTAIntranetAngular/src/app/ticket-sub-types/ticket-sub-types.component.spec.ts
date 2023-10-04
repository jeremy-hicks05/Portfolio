import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketSubTypesComponent } from './ticket-sub-types.component';

describe('TicketSubTypesComponent', () => {
  let component: TicketSubTypesComponent;
  let fixture: ComponentFixture<TicketSubTypesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TicketSubTypesComponent]
    });
    fixture = TestBed.createComponent(TicketSubTypesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
