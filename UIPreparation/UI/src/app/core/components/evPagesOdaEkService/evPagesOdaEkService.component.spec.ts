/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EvPagesOdaEkServiceComponent } from './evPagesOdaEkService.component';

describe('EvPagesOdaEkServiceComponent', () => {
  let component: EvPagesOdaEkServiceComponent;
  let fixture: ComponentFixture<EvPagesOdaEkServiceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EvPagesOdaEkServiceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EvPagesOdaEkServiceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
