import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoBueiroComponent } from './info-bueiro.component';

describe('InfoBueiroComponent', () => {
  let component: InfoBueiroComponent;
  let fixture: ComponentFixture<InfoBueiroComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InfoBueiroComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InfoBueiroComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
