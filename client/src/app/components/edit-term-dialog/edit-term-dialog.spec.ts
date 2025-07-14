import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditTermDialog } from './edit-term-dialog';

describe('EditTermDialog', () => {
  let component: EditTermDialog;
  let fixture: ComponentFixture<EditTermDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditTermDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditTermDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
