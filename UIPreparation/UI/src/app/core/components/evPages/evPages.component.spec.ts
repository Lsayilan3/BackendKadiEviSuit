/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EvPagesComponent } from './evPages.component';

describe('EvPagesComponent', () => {
  let component: EvPagesComponent;
  let fixture: ComponentFixture<EvPagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EvPagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EvPagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
