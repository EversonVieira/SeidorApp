import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseSeidorComponentComponent } from './base-seidor-component.component';

describe('BaseSeidorComponentComponent', () => {
  let component: BaseSeidorComponentComponent;
  let fixture: ComponentFixture<BaseSeidorComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BaseSeidorComponentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseSeidorComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
