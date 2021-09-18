BEGIN;

SET client_encoding = 'UTF8';

CREATE SCHEMA IF NOT EXISTS pgcrypto;

SET SCHEMA 'pgcrypto';

CREATE EXTENSION IF NOT EXISTS pgcrypto; 

CREATE SCHEMA IF NOT EXISTS info;
SET SCHEMA 'info';

CREATE TABLE IF NOT EXISTS clerkdata.info.committee (
    committee_id UUID PRIMARY KEY,
    committee_code VARCHAR(10) NOT NULL,
    committee_type VARCHAR(50) NOT NULL,
    committee_room VARCHAR(50) NOT NULL,
    committee_header_text TEXT NOT NULL,
    committee_zip VARCHAR(10) NOT NULL,
    committee_zip_suffix VARCHAR(10) NOT NULL,
    committee_building_code VARCHAR(50) NOT NULL,
    committee_phone VARCHAR(20) NOT NULL,
    committee_full_name TEXT NOT NULL,
    committee_majority INTEGER NOT NULL,
    committee_minority INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS clerkdata.info.subcommittee (
    subcommittee_id UUID PRIMARY KEY,
    subcommittee_code VARCHAR(10) NOT NULL,
    subcommittee_room VARCHAR(50) NOT NULL,
    subcommittee_zip VARCHAR(10) NOT NULL,
    subcommittee_zip_suffix VARCHAR(10) NOT NULL,
    subcommittee_building_code VARCHAR(50) NOT NULL,
    subcommittee_phone VARCHAR(20) NOT NULL,
    subcommittee_full_name TEXT NOT NULL,
    subcommittee_majority INTEGER NOT NULL,
    subcommittee_minority INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS clerkdata.info.member (
    member_id UUID PRIMARY KEY,
    state_district VARCHAR(10) NOT NULL,
    bioguide_id VARCHAR(20) NOT NULL,
    last_name VARCHAR(20) NOT NULL,
    first_name VARCHAR(20) NOT NULL,
    middle_name VARCHAR(20),
    suffix VARCHAR(10),
    courtesy VARCHAR(10),
    name_list VARCHAR(50),
    sort_name VARCHAR(50),
    official_name VARCHAR(50),
    formal_name VARCHAR(50),
    prior_congress INTEGER,
    party VARCHAR(100) NOT NULL,
    caucus VARCHAR(100) NOT NULL,
    representing_state VARCHAR(10) NOT NULL,
    district VARCHAR(50) NOT NULL,
    town_name VARCHAR(100) NOT NULL,
    office_building VARCHAR(100) NOT NULL,
    office_room INTEGER NOT NULL,
    office_zip VARCHAR(10) NOT NULL,
    office_zip_suffix VARCHAR(10) NOT NULL,
    office_phone_number VARCHAR(20) NOT NULL,
    elected_date VARCHAR(50) NOT NULL,
    sworn_date VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS clerkdata.info.nationstate (
    state_id UUID PRIMARY KEY,
    state_postal_code VARCHAR(10) NOT NULL,
    state_name VARCHAR(20) NOT NULL
);

CREATE TABLE IF NOT EXISTS clerkdata.info.memberdata (
    memberdata_id UUID PRIMARY KEY,
    publish_date VARCHAR(50) NOT NULL,
    congress_num INTEGER NOT NULL,
    congress_text TEXT NOT NULL,
    meeting_session INTEGER NOT NULL,
    majority VARCHAR(10) NOT NULL,
    minority VARCHAR(10) NOT NULL,
    clerk VARCHAR(50) NOT NULL,
    web_url TEXT NOT NULL
);


CREATE SCHEMA IF NOT EXISTS data;
SET SCHEMA 'data';

CREATE TABLE IF NOT EXISTS clerkdata.data.committeesubcommitteeassociation (
    committee_code VARCHAR(10) NOT NULL,
    subcommittee_code VARCHAR(10) NOT NULL,
);

CREATE TABLE IF NOT EXISTS clerkdata.data.membercommitteeassociation (
    bioguide_id VARCHAR(20) NOT NULL,
    committee_code VARCHAR(10) NOT NULL,
    member_rank VARCHAR(10) NOT NULL,
    is_sub_committee BOOLEAN NOT NULL
);

CREATE TABLE IF NOT EXISTS clerkdata.data.memberdatamemberassociation (
    congress_num INTEGER NOT NULL,
    bioguide_id VARCHAR(20) NOT NULL
);

CREATE TABLE IF NOT EXISTS clerkdata.data.memberdatacommitteeassociation (
    congress_num INTEGER NOT NULL,
    committee_code VARCHAR(10) NOT NULL,
);

COMMIT;