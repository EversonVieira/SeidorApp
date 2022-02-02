import { TestBed } from '@angular/core/testing';

import { CPFService } from './cpf.service';

describe('CPFService', () => {
  let service: CPFService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CPFService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
