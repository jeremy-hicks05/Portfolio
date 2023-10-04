import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApprovalStatesComponent } from './approval-states.component';

describe('ApprovalStatesComponent', () => {
  let component: ApprovalStatesComponent;
  let fixture: ComponentFixture<ApprovalStatesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ApprovalStatesComponent]
    });
    fixture = TestBed.createComponent(ApprovalStatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
