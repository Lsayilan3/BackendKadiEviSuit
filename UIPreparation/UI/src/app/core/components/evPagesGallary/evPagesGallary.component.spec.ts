/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EvPagesGallaryComponent } from './evPagesGallary.component';

describe('EvPagesGallaryComponent', () => {
  let component: EvPagesGallaryComponent;
  let fixture: ComponentFixture<EvPagesGallaryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EvPagesGallaryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EvPagesGallaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
