import { TestBed } from '@angular/core/testing';

import { GlossaryTermService } from './glossary-term-service';

describe('GlossaryTermService', () => {
  let service: GlossaryTermService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlossaryTermService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
