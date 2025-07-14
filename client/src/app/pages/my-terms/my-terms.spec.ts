import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyTerms } from './my-terms';

describe('MyTerms', () => {
  let component: MyTerms;
  let fixture: ComponentFixture<MyTerms>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MyTerms]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyTerms);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
