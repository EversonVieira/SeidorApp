import { TestBed } from '@angular/core/testing';

import { BlobToB64Service } from './blob-to-b64.service';

describe('BlobToB64Service', () => {
  let service: BlobToB64Service;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlobToB64Service);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
