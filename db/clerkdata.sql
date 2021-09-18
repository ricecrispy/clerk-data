-- This SQL script sets up the clerk data db

BEGIN;

SET client_encoding = 'UTF8';

-- import pgcrypto to auto-generate UUIDs
CREATE SCHEMA IF NOT EXISTS pgcrypto;
SET SCHEMA 'pgcrypto';
CREATE EXTENSION IF NOT EXISTS pgcrypto; 

-- create schema info to hold non-relational data
CREATE SCHEMA IF NOT EXISTS info;
SET SCHEMA 'info';

-- create table for the committee object
CREATE TABLE IF NOT EXISTS clerkdata.info.committee (
    committee_id UUID PRIMARY KEY,
    committee_code VARCHAR(10) NOT NULL,
    committee_type VARCHAR(50) NOT NULL,
    committee_room VARCHAR(50) NOT NULL,
    committee_header_text TEXT,
    committee_zip VARCHAR(10) NOT NULL,
    committee_zip_suffix VARCHAR(10) NOT NULL,
    committee_building_code VARCHAR(50) NOT NULL,
    committee_phone VARCHAR(20) NOT NULL,
    committee_full_name TEXT NOT NULL,
    committee_majority INTEGER NOT NULL,
    committee_minority INTEGER NOT NULL
);

create function udf_select_committees()
returns TABLE (
    committee_id UUID,
    committee_code VARCHAR(10),
    committee_type VARCHAR(50),
    committee_room VARCHAR(50),
    committee_header_text TEXT,
    committee_zip VARCHAR(10),
    committee_zip_suffix VARCHAR(10),
    committee_building_code VARCHAR(50),
    committee_phone VARCHAR(20),
    committee_full_name TEXT,
    committee_majority INTEGER,
    committee_minority INTEGER
)
language plpgsql
as
$$
declare
begin
	RETURN QUERY
	SELECT * from info.committee;
end;
$$;

create function udf_select_committee_by_committee_code(p_committee_code VARCHAR(10))
returns TABLE (
    committee_id UUID,
    committee_code VARCHAR(10),
    committee_type VARCHAR(50),
    committee_room VARCHAR(50),
    committee_header_text TEXT,
    committee_zip VARCHAR(10),
    committee_zip_suffix VARCHAR(10),
    committee_building_code VARCHAR(50),
    committee_phone VARCHAR(20),
    committee_full_name TEXT,
    committee_majority INTEGER,
    committee_minority INTEGER
)
language plpgsql
as
$$
declare
begin
	RETURN QUERY
	SELECT * from info.committee c
    WHERE c.committee_code = p_committee_code;
end;
$$;

create function udf_create_committee(
    p_code VARCHAR(10),
    p_type VARCHAR(50),
    p_room VARCHAR(50),
    p_header_text TEXT,
    p_zip VARCHAR(10),
    p_zip_suffix VARCHAR(10),
    p_building_code VARCHAR(50),
    p_phone VARCHAR(20),
    p_full_name TEXT,
    p_majority INTEGER,
    p_minority INTEGER
)
returns void
language plpgsql
as
$$
declare
    committee_id UUID;
BEGIN
    CREATE EXTENSION IF NOT EXISTS pgcrypto;
	committee_id := (SELECT gen_random_uuid
                        FROM pgcrypto.gen_random_uuid());

    INSERT INTO info.committee(
        committee_id,
        committee_code,
        committee_type,
        committee_room,
        committee_header_text,
        committee_zip,
        committee_zip_suffix,
        committee_building_code,
        committee_phone,
        committee_full_name,
        committee_majority,
        committee_minority
    )
    VALUES(
        committee_id,
        p_code,
        p_type,
        p_room,
        p_header_text,
        p_zip,
        p_zip_suffix,
        p_building_code,
        p_phone,
        p_full_name,
        p_majority,
        p_minority
    );
END;
$$;

-- create table for the subcommittee object
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

create function udf_create_sub_committee(
    p_code VARCHAR(10),
    p_room VARCHAR(50),
    p_zip VARCHAR(10),
    p_zip_suffix VARCHAR(10),
    p_building_code VARCHAR(50),
    p_phone VARCHAR(20),
    p_full_name TEXT,
    p_majority INTEGER,
    p_minority INTEGER
)
returns void
language plpgsql
as
$$
declare
    subcommittee_id UUID;
BEGIN
    CREATE EXTENSION IF NOT EXISTS pgcrypto;
	subcommittee_id := (SELECT gen_random_uuid
                        FROM pgcrypto.gen_random_uuid());

    INSERT INTO info.subcommittee(
        subcommittee_id,
        subcommittee_code,
        subcommittee_room,
        subcommittee_zip,
        subcommittee_zip_suffix,
        subcommittee_building_code,
        subcommittee_phone,
        subcommittee_full_name,
        subcommittee_majority,
        subcommittee_minority
    )
    VALUES(
        subcommittee_id,
        p_code,
        p_room,
        p_zip,
        p_zip_suffix,
        p_building_code,
        p_phone,
        p_full_name,
        p_majority,
        p_minority
    );
END;
$$;

-- create table for the member object
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

create function udf_select_members()
returns TABLE (
    member_id UUID,
    state_district VARCHAR(10),
    bioguide_id VARCHAR(20),
    last_name VARCHAR(20),
    first_name VARCHAR(20),
    middle_name VARCHAR(20),
    suffix VARCHAR(10),
    courtesy VARCHAR(10),
    name_list VARCHAR(50),
    sort_name VARCHAR(50),
    official_name VARCHAR(50),
    formal_name VARCHAR(50),
    prior_congress INTEGER,
    party VARCHAR(100),
    caucus VARCHAR(100),
    representing_state VARCHAR(10),
    district VARCHAR(50),
    town_name VARCHAR(100),
    office_building VARCHAR(100),
    office_room INTEGER,
    office_zip VARCHAR(10),
    office_zip_suffix VARCHAR(10),
    office_phone_number VARCHAR(20),
    elected_date VARCHAR(50),
    sworn_date VARCHAR(50)
)
language plpgsql
as
$$
declare
begin
	RETURN QUERY
	SELECT * from info.member;
end;
$$;

create function udf_select_member_by_bioguide_id(p_bioguide_id VARCHAR(20))
returns TABLE (
    member_id UUID,
    state_district VARCHAR(10),
    bioguide_id VARCHAR(20),
    last_name VARCHAR(20),
    first_name VARCHAR(20),
    middle_name VARCHAR(20),
    suffix VARCHAR(10),
    courtesy VARCHAR(10),
    name_list VARCHAR(50),
    sort_name VARCHAR(50),
    official_name VARCHAR(50),
    formal_name VARCHAR(50),
    prior_congress INTEGER,
    party VARCHAR(100),
    caucus VARCHAR(100),
    representing_state VARCHAR(10),
    district VARCHAR(50),
    town_name VARCHAR(100),
    office_building VARCHAR(100),
    office_room INTEGER,
    office_zip VARCHAR(10),
    office_zip_suffix VARCHAR(10),
    office_phone_number VARCHAR(20),
    elected_date VARCHAR(50),
    sworn_date VARCHAR(50)
)
language plpgsql
as
$$
declare
begin
	RETURN QUERY
	SELECT * from info.member m
    WHERE m.bioguide_id = p_bioguide_id;
end;
$$;

create function udf_create_member(
    p_state_district VARCHAR(10),
    p_bioguide_id VARCHAR(20),
    p_last_name VARCHAR(20),
    p_first_name VARCHAR(20),
    p_middle_name VARCHAR(20),
    p_suffix VARCHAR(10),
    p_courtesy VARCHAR(10),
    p_name_list VARCHAR(50),
    p_sort_name VARCHAR(50),
    p_official_name VARCHAR(50),
    p_formal_name VARCHAR(50),
    p_prior_congress INTEGER,
    p_party VARCHAR(100),
    p_caucus VARCHAR(100),
    p_state VARCHAR(10),
    p_district VARCHAR(50),
    p_town_name VARCHAR(100),
    p_office_building VARCHAR(100),
    p_office_room INTEGER,
    p_office_zip VARCHAR(10),
    p_office_zip_suffix VARCHAR(10),
    p_phone_number VARCHAR(20),
    p_elected_date VARCHAR(50),
    p_sworn_date VARCHAR(50)
)
returns void
language plpgsql
as
$$
declare
    member_id UUID;
BEGIN
    CREATE EXTENSION IF NOT EXISTS pgcrypto;
	member_id := (SELECT gen_random_uuid
                        FROM pgcrypto.gen_random_uuid());

    INSERT INTO info.member(
        member_id,
        state_district,
        bioguide_id,
        last_name,
        first_name,
        middle_name,
        suffix,
        courtesy,
        name_list,
        sort_name,
        official_name,
        formal_name,
        prior_congress,
        party,
        caucus,
        representing_state,
        district,
        town_name,
        office_building,
        office_room,
        office_zip,
        office_zip_suffix,
        office_phone_number,
        elected_date,
        sworn_date
    )
    VALUES(
        member_id,
        p_state_district,
        p_bioguide_id,
        p_last_name,
        p_first_name,
        p_middle_name,
        p_suffix,
        p_courtesy,
        p_name_list,
        p_sort_name,
        p_official_name,
        p_formal_name,
        p_prior_congress,
        p_party,
        p_caucus,
        p_state,
        p_district,
        p_town_name,
        p_office_building,
        p_office_room,
        p_office_zip,
        p_office_zip_suffix,
        p_phone_number,
        p_elected_date,
        p_sworn_date
    );
END;
$$;


-- create table for the state object
CREATE TABLE IF NOT EXISTS clerkdata.info.nationstate (
    state_postal_code VARCHAR(10) NOT NULL,
    state_name VARCHAR(50) NOT NULL
);

-- pre-load the 50 states' postal code and full name into the nationstate table 
INSERT INTO nationstate (state_postal_code, state_name)
VALUES
('AL', 'Alabama'),
('AK', 'Alaska'),
('AZ', 'Arizona'),
('AR', 'Arkansas'),
('CA', 'California'),
('CO', 'Colorado'),
('CT', 'Connecticut'),
('DE', 'Delaware'),
('DC', 'District of Columbia'),
('FL', 'Florida'),
('GA', 'Georgia'),
('HI', 'Hawaii'),
('ID', 'Idaho'),
('IL', 'Illinois'),
('IN', 'Indiana'),
('IA', 'Iowa'),
('KS', 'Kansas'),
('KY', 'Kentucky'),
('LA', 'Louisiana'),
('ME', 'Maine'),
('MD', 'Maryland'),
('MA', 'Massachusetts'),
('MI', 'Michigan'),
('MN', 'Minnesota'),
('MS', 'Mississippi'),
('MO', 'Missouri'),
('MT', 'Montana'),
('NE', 'Nebraska'),
('NV', 'Nevada'),
('NH', 'New Hampshire'),
('NJ', 'New Jersey'),
('NM', 'New Mexico'),
('NY', 'New York'),
('NC', 'North Carolina'),
('ND', 'North Dakota'),
('OH', 'Ohio'),
('OK', 'Oklahoma'),
('OR', 'Oregon'),
('PA', 'Pennsylvania'),
('RI', 'Rhode Island'),
('SC', 'South Carolina'),
('SD', 'South Dakota'),
('TN', 'Tennessee'),
('TX', 'Texas'),
('UT', 'Utah'),
('VT', 'Vermont'),
('VA', 'Virginia'),
('WA', 'Washington'),
('WV', 'West Virginia'),
('WI', 'Wisconsin'),
('WY', 'Wyoming')
;

create function udf_select_states()
returns TABLE (
    state_postal_code VARCHAR(10),
    state_name VARCHAR(50)
)
language plpgsql
as
$$
declare
BEGIN
    RETURN QUERY
    SELECT * FROM info.nationstate;
END;
$$; 

-- create table for the memberdata object
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

create function udf_create_memberdata(
    p_publish_date VARCHAR(50),
    p_congress_num INTEGER,
    p_congress_text TEXT,
    p_session INTEGER,
    p_majority VARCHAR(10),
    p_minority VARCHAR(10),
    p_clerk VARCHAR(50),
    p_web_url TEXT
)
returns void
language plpgsql
as
$$
declare
    memberdata_id UUID;
BEGIN
    CREATE EXTENSION IF NOT EXISTS pgcrypto;
    memberdata_id := (SELECT gen_random_uuid FROM pgcrypto.gen_random_uuid());

    INSERT INTO info.memberdata(
        memberdata_id,
        publish_date,
        congress_num,
        congress_text,
        meeting_session,
        majority,
        minority,
        clerk,
        web_url
    )
    VALUES (
        memberdata_id,
        p_publish_date,
        p_congress_num,
        p_congress_text,
        p_session,
        p_majority,
        p_minority,
        p_clerk,
        p_web_url
    );
END;
$$;

create function udf_select_memberdata_by_congress_num_and_session(
    p_congress_num INTEGER,
    p_session INTEGER)
returns TABLE (
    memberdata_id UUID,
    publish_date VARCHAR(50),
    congress_num INTEGER,
    congress_text TEXT,
    meeting_session INTEGER,
    majority VARCHAR(10),
    minority VARCHAR(10),
    clerk VARCHAR(50),
    web_url TEXT
)
language plpgsql
as
$$
declare
BEGIN
    RETURN QUERY
    SELECT * FROM info.memberdata md
    WHERE md.congress_num = p_congress_num
    AND md.meeting_session = p_session;
END;
$$;

create function udf_select_memberdata()
returns TABLE (
    memberdata_id UUID,
    publish_date VARCHAR(50),
    congress_num INTEGER,
    congress_text TEXT,
    meeting_session INTEGER,
    majority VARCHAR(10),
    minority VARCHAR(10),
    clerk VARCHAR(50),
    web_url TEXT
)
language plpgsql
as
$$
declare
BEGIN
    RETURN QUERY
    SELECT * FROM info.memberdata;
END;
$$;

-- create schema for relational data
CREATE SCHEMA IF NOT EXISTS data;
SET SCHEMA 'data';

-- create table to represent association between committee and subcommittee
CREATE TABLE IF NOT EXISTS clerkdata.data.committeesubcommitteeassociation (
    committee_code VARCHAR(10) NOT NULL,
    subcommittee_code VARCHAR(10) NOT NULL
);

create function udf_select_subcommittee_by_committee_code(p_committee_code VARCHAR(10))
returns TABLE (
    subcommittee_id UUID,
    subcommittee_code VARCHAR(10),
    subcommittee_room VARCHAR(50),
    subcommittee_zip VARCHAR(10),
    subcommittee_zip_suffix VARCHAR(10),
    subcommittee_building_code VARCHAR(50),
    subcommittee_phone VARCHAR(20),
    subcommittee_full_name TEXT,
    subcommittee_majority INTEGER,
    subcommittee_minority INTEGER
)
language plpgsql
as
$$
declare
BEGIN
    RETURN QUERY
    SELECT sc.* FROM info.subcommittee sc
    JOIN data.committeesubcommitteeassociation csa
    ON csa.subcommittee_code = sc.subcommittee_code
    WHERE csa.committee_code = p_committee_code;
END;
$$;

create function udf_committee_associate_sub_committee(p_committee_code VARCHAR(10), p_sub_committee_code VARCHAR(10))
returns void
language plpgsql
as
$$
declare
BEGIN
    INSERT INTO data.committeesubcommitteeassociation(committee_code, subcommittee_code)
    VALUES(p_committee_code, p_sub_committee_code);
END;
$$;

-- create table to represent association between member and committee
CREATE TABLE IF NOT EXISTS clerkdata.data.membercommitteeassociation (
    bioguide_id VARCHAR(20) NOT NULL,
    committee_code VARCHAR(10) NOT NULL,
    member_rank VARCHAR(10) NOT NULL,
    is_sub_committee BOOLEAN NOT NULL
);

create function udf_associate_member_committeeAssignment(
    p_bioguide_id VARCHAR(20),
    p_committee_code VARCHAR(10),
    p_rank VARCHAR(10),
    p_is_sub_committee BOOLEAN)
returns void
language plpgsql
as
$$
declare
BEGIN
    INSERT INTO data.membercommitteeassociation(bioguide_id, committee_code, member_rank, is_sub_committee)
    VALUES (p_bioguide_id, p_committee_code, p_rank, p_is_sub_committee);
END;
$$;

-- create table to represent association between memberdata and member
CREATE TABLE IF NOT EXISTS clerkdata.data.memberdatamemberassociation (
    congress_num INTEGER NOT NULL,
    bioguide_id VARCHAR(20) NOT NULL
);

create function udf_associate_member_memberdata(p_congress_num INTEGER, p_member_biograde_id VARCHAR(20))
returns void
language plpgsql
as
$$
declare
BEGIN
    INSERT INTO data.memberdatamemberassociation(congress_num, bioguide_id)
    VALUES (p_congress_num, p_member_biograde_id);
END;
$$;

-- create table to represent association between memberdata and committee
CREATE TABLE IF NOT EXISTS clerkdata.data.memberdatacommitteeassociation (
    congress_num INTEGER NOT NULL,
    committee_code VARCHAR(10) NOT NULL
);

create function udf_associate_committee_memberdata(p_congress_num INTEGER, p_committee_code VARCHAR(10))
returns void
language plpgsql
as
$$
declare
BEGIN
    INSERT INTO data.memberdatacommitteeassociation(congress_num, committee_code)
    VALUES (p_congress_num, p_committee_code);
END;
$$;


COMMIT;