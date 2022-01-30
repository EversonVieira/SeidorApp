import { TestBed } from '@angular/core/testing';

import { AppInjectorService } from './app-injector.service';

describe('AppInjectorService', () => {
  let service: AppInjectorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppInjectorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
