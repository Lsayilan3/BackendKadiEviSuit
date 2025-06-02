/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EvPagesOdaOlanaklarComponent } from './evPagesOdaOlanaklar.component';

describe('EvPagesOdaOlanaklarComponent', () => {
  let component: EvPagesOdaOlanaklarComponent;
  let fixture: ComponentFixture<EvPagesOdaOlanaklarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EvPagesOdaOlanaklarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EvPagesOdaOlanaklarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
