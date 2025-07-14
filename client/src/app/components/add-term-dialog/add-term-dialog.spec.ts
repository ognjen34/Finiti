import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddTermDialog } from './add-term-dialog';

describe('AddTermDialog', () => {
  let component: AddTermDialog;
  let fixture: ComponentFixture<AddTermDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddTermDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddTermDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
