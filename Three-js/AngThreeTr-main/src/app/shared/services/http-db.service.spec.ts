import { TestBed } from '@angular/core/testing';

import { HttpDBService } from './http-db.service';

describe('HttpDbService', () => {
  let service: HttpDBService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HttpDBService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
